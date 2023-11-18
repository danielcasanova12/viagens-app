import React, { useEffect, useMemo, useState } from "react";
import debounce from "lodash/debounce";
import {
	HotelService
} from "../../shared/services/api";
import {
	IconButton,
	ImageList,
	ImageListItem,
	ImageListItemBar,
	ListSubheader,
	useMediaQuery,
	useTheme,
} from "@mui/material";
import { LayoutBasePage } from "../../shared/layouts";
import { ToolsList } from "../../shared/components";
import { useSearchParams } from "react-router-dom";
import Pagination from "@mui/material/Pagination";
import Stack from "@mui/material/Stack";
import { Environment } from "../../shared/environment/Environments";
import { useNavigate  } from "react-router-dom";
import { IHotel } from "../../shared/Interfaces/Interfaces";

export const Voos = () => {
	const [hotels, setHotels] = useState<IHotel[]>([]);
	const [error, setError] = useState<Error | null>(null);
	const theme = useTheme();
	const smDown = useMediaQuery(theme.breakpoints.down("sm"));
	const mdDown = useMediaQuery(theme.breakpoints.down("md"));
	const [searchParams, setSearchParams] = useSearchParams();
	const [totalCount, setTotalCount] = useState(0);
	const navigate = useNavigate();

	const search = useMemo(() => {
		return searchParams.get("search") || "";
	}, [searchParams]);

	const page = useMemo(() => {
		return Number(searchParams.get("page") || "1");
	}, [searchParams]);

	const fetchData = async (searchValue: string, pageNumber: number) => {
		try {
			const data = await HotelService.getAllHotels(searchValue, pageNumber);
			if (data instanceof Error) {
				setError(data);
			} else {
				if (Array.isArray(data) ) {
					const extractedHotels = data[0].data;
					setHotels(extractedHotels);
					setTotalCount(data[0].totalCount);
				} else {
					// Trate o caso em que não há hotéis disponíveis, por exemplo:
					setHotels([]);
					setTotalCount(0);
				}
				
				
			}
		} catch (e) {
			setError(error);
		}
	
	};

	// Use o debounce para controlar as chamadas à API
	const debouncedFetchData = debounce(fetchData, 500);

	useEffect(() => {
		// Chame a função debouncedFetchData sempre que a busca ou a página mudar
		debouncedFetchData(search, page);
	}, [search, page]);

	const getImageCols = () => {
		if (smDown) {
			return 1;
		} else if (mdDown) {
			return 3;
		} else {
			return 5;
		}
	};

	return (
		<div>
			<LayoutBasePage
				title="Hotels"
				toolbar={
					<ToolsList
						showInputSearch={true}
						showSaveButton={false}
						changeTextSearch={(text) => setSearchParams({ search: text, page: "1" }, { replace: true })}
						textSearch={search}
					/>
				}
			>
				{error && <p>{error.message}</p>}
				<ImageList sx={{ width: "100%", height: "auto" }} cols={getImageCols()}>
					<ImageListItem key="Subheader" cols={getImageCols()}>
						<ListSubheader component="div">Hotels</ListSubheader>
					</ImageListItem>
					{hotels.length > 0 ? (
						hotels
							.filter((item) => item.images.length > 0)
							.map((item) => (
								<ImageListItem key={item.idHotel}>
									<img
										srcSet={`${item.images[0].imageUrl}?w=248&fit=crop&auto=format&dpr=2 2x`}
										src={`${item.images[0].imageUrl}?w=248&fit=crop&auto=format`}
										alt={item.name}
										loading="lazy"
									/>
									<ImageListItemBar
										title={item.name}
										subtitle={item.location?.city}
										actionIcon={
											<IconButton
												sx={{ color: "rgba(255, 255, 255, 0.54)" }}
												aria-label={`info about ${item.name}`}
												onClick={() => navigate(`/hotels/detalhes/${item.idHotel}`)}
											>
              Detathes
											</IconButton>
										}
									/>
								</ImageListItem>
							))
					) : (
						<p>Nenhum hotel disponível.</p>
					)}

				</ImageList>
				<Stack spacing={4}>
					<Pagination
						count={Math.ceil(totalCount / Environment.LIMIT_DEFAULT)}
						color="primary"
						page={page}
						onChange={(event, value) => {
							setSearchParams({ page: String(value) }, { replace: true });
						}}
					/>
				</Stack>
			</LayoutBasePage>
		</div>
	);
};
