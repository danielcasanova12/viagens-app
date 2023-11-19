import { useState, useMemo, useEffect } from "react";
import { useTheme } from "@mui/material/styles";
import useMediaQuery from "@mui/material/useMediaQuery";
import { useSearchParams } from "react-router-dom";
import { useNavigate } from "react-router-dom";
import { ImageList, ImageListItem, ImageListItemBar, IconButton, ListSubheader, Stack } from "@mui/material";
import { Pagination } from "@mui/lab";
import { ToolsList } from "../../shared/components/";
import { Environment } from "../../shared/environment/Environments";
import { CarService } from "../../shared/services/api/car/CarService";
import { ICarRental } from "../../shared/Interfaces/Interfaces";
import debounce from "lodash.debounce";
import { LayoutBasePage } from "../../shared/layouts";

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
	const fetchData = async (searchValue: string, pageNumber: number) => {
		try {
			const data = await CarService.getAllCarRentals(searchValue,pageNumber);
			console.log("data",data);
			if (data instanceof Error) {
				setError(data);
				console.log(searchValue);
			} else {
				if (Array.isArray(data) ) {
					const extractedCarRentals = data[0].carro;
					setCarRentals(extractedCarRentals);
					setTotalCount(data[0].contAllCars);
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
					{carRentals && carRentals.length > 0 ? (
						carRentals.map((item) => (
							item.image && (
								<ImageListItem key={item.idCarRental}>
									<img src={item.image} alt={item.model ? item.model : undefined} />
									<ImageListItemBar
										title={item.model}
										subtitle={item.company}
										actionIcon={
											<IconButton
												sx={{ color: "rgba(255, 255, 255, 0.54)" }}
												aria-label={`info about ${item.model}`}
												onClick={() => navigate(`/car/detalhes/${item.idCarRental}`)}
											>
                                Detalhes
											</IconButton>
										}
									/>
								</ImageListItem>
							)
						))
					) : (
						<p>Nenhum aluguel de carro disponível.</p>
					)}
				</ImageList>

				<Stack spacing={4}>
					<Pagination
						count={isNaN(totalCount / Environment.LIMIT_DEFAULT) ? 0 : Math.ceil(totalCount / Environment.LIMIT_DEFAULT)}
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
