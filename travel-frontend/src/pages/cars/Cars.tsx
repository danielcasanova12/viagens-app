import { useState, useMemo, useEffect } from "react";
import { useTheme } from "@mui/material/styles";
import useMediaQuery from "@mui/material/useMediaQuery";
import { useSearchParams } from "react-router-dom";
import { useNavigate } from "react-router-dom";
import { ImageList, ImageListItem, ImageListItemBar, IconButton, ListSubheader, Stack } from "@mui/material";
import { Pagination } from "@mui/lab";
import { ToolsList, LayoutBasePage } from "../../../components";
import { Environment } from "../../../environment/Environments";
import { CarService } from "../../../services";
import { ICarRental } from "../../../Interfaces/Interfaces";
import debounce from "lodash.debounce";

export const Cars = () => {
	const [carRentals, setCarRentals] = useState<ICarRental[]>([]);
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

	const fetchData = async (pageNumber: number) => {
		try {
			const data = await CarService.getAllCarRentals(pageNumber);
			if (data instanceof Error) {
				setError(data);
			} else {
				if (Array.isArray(data) ) {
					const extractedCarRentals = data[0].data;
					setCarRentals(extractedCarRentals);
					setTotalCount(data[0].totalCount);
				} else {
					setCarRentals([]);
					setTotalCount(0);
				}
			}
		} catch (e) {
			setError(error);
		}
	};

	const debouncedFetchData = debounce(fetchData, 500);

	useEffect(() => {
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
				title="Car Rentals"
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
						<ListSubheader component="div">Car Rentals</ListSubheader>
					</ImageListItem>
					{carRentals.length > 0 ? (
						carRentals.map((item) => (
							<ImageListItem key={item.IdCarRental}>
								<img
									srcSet={`${item.PickupLocation.imageUrl}?w=248&fit=crop&auto=format&dpr=2 2x`}
									src={`${item.PickupLocation.imageUrl}?w=248&fit=crop&auto=format`}
									alt={item.Model}
									loading="lazy"
								/>
								<ImageListItemBar
									title={item.Model}
									subtitle={item.Company}
									actionIcon={
										<IconButton
											sx={{ color: "rgba(255, 255, 255, 0.54)" }}
											aria-label={`info about ${item.Model}`}
											onClick={() => navigate(`/carrentals/detalhes/${item.IdCarRental}`)}
										>
											Detalhes
										</IconButton>
									}
								/>
							</ImageListItem>
						))
					) : (
						<p>Nenhum aluguel de carro disponível.</p>
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
