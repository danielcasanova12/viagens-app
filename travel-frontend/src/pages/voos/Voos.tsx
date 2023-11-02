import React, { useEffect, useMemo, useState } from "react";
import {
	HotelService,
	IHotel,
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

export const Voos = () => {
	const [hotels, setHotels] = useState<IHotel[]>([]);
	const [loading, setLoading] = useState(false);
	const [error, setError] = useState<Error | null>(null);
	const theme = useTheme();
	const smDown = useMediaQuery(theme.breakpoints.down("sm"));
	const mdDown = useMediaQuery(theme.breakpoints.down("md"));
	const [searchParams, setSearchParams] = useSearchParams();
	useEffect(() => {
		setLoading(true);
		HotelService.getAllHotels()
			.then((data) => {
				if (data instanceof Error) {
					setError(data);
				} else {
					setHotels(data);
					console.log("Dados recebidos com sucesso!");
				}
			})
			.catch((error) => {
				setError(error);
			})
			.finally(() => {
				setLoading(false);
			});
	}, []);

	const search = useMemo(() => {
		return searchParams.get("search") || "";
	}, [searchParams]);

	const filteredHotels = useMemo(() => {
		// Realize a filtragem com base no termo de pesquisa
		return hotels.filter((hotel) => {
			const nameLower = hotel.name.toLowerCase();
			const cityLower = hotel.location?.city.toLowerCase();
			const searchLower = search.toLowerCase();
		
			// Verifica se o termo de pesquisa está presente no nome ou na cidade do hotel
			return nameLower.includes(searchLower) || cityLower?.includes(searchLower);
		});
		
	}, [hotels, search]);

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
				title="Voos"
				toolbar={
					<ToolsList
						showInputSearch={true}
						textSearch={search}
						changeTextSearch={(text) => setSearchParams({ search: text }, { replace: true })}
						textSaveButton="Filtrar" // Renomeie o botão de busca para "Filtrar"
					/>
				}
			>
				{loading && <p>Loading...</p>}
				{error && <p>{error.message}</p>}
				<ImageList sx={{ width: "100%", height: "auto" }} cols={getImageCols()}>
					<ImageListItem key="Subheader" cols={getImageCols()}>
						<ListSubheader component="div">December</ListSubheader>
					</ImageListItem>
					{filteredHotels.map((item) => (
						<ImageListItem key={item.idHotel}>
							<img
								srcSet={`${item.image}?w=248&fit=crop&auto=format&dpr=2 2x`}
								src={`${item.image}?w=248&fit=crop&auto=format`}
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
									>
                    teste
									</IconButton>
								}
							/>
						</ImageListItem>
					))}
				</ImageList>
			</LayoutBasePage>
		</div>
		
	);
};
