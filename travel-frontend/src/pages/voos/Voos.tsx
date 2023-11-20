
import { useState,useEffect, useMemo } from "react";
import { ToolsList } from "../../shared/components/";
import { LayoutBasePage } from "../../shared/layouts";
import { Button, Card, CardActions, CardContent, CardMedia, Grid,Stack, Typography, debounce, useTheme } from "@mui/material";
import { FlightService } from "../../shared/services/api/flights/FlightsService";
import { IFlight } from "../../shared/Interfaces/Interfaces";
import { useNavigate, useSearchParams } from "react-router-dom";

export const Voos = () => {
	const [searchParams, setSearchParams] = useSearchParams();
	const [flights, setFlights] = useState<IFlight[]>([]);
	const theme = useTheme();
	const navigate = useNavigate();
	const search = useMemo(() => {
		return searchParams.get("search") || "";
	}, [searchParams]);

	const page = useMemo(() => {
		return Number(searchParams.get("page") || "1");
	}, [searchParams]);
	
	const getBackgroundColor = () => {
		// Use o tema aqui para ajustar a cor de fundo com base no tema atual
		return theme.palette.background.default;
	};
	const fetchData = async (searchValue: string, pageNumber: number) => {
		try {
			const result = await FlightService.getAllFlights(searchValue, pageNumber);
			if (result instanceof Error) {
				console.error(result);
			} else {
				if (Array.isArray(result) ) {
					setFlights(result[0].flighti);
				} else {
					// Trate o caso em que não há hotéis disponíveis, por exemplo:
					setFlights([]);
				}
				
				
			}
		} catch (e) {
			console.error(e);
		}
	
	};

	
	const handleDetails = (id: number) => {
		// Implemente esta função para lidar com a ação quando o botão "Detalhes" é clicado
		console.log(`Detalhes do voo ${id}`);
		getBackgroundColor();
		navigate(`/voos/detalhes/${id}`);
	};
	const debouncedFetchData = debounce(fetchData, 500);

	useEffect(() => {
		// Chame a função debouncedFetchData sempre que a busca ou a página mudar
		debouncedFetchData(search, page);
	}, [search, page]);
	
	const getImageSize = () => {
		// Implemente esta função para retornar o tamanho da imagem
	};


	return (
		<div>
					 			<LayoutBasePage
				title="Voos"
				toolbar={
					<ToolsList
						showInputSearch={true}
						showSaveButton={false}
						changeTextSearch={(text) => setSearchParams({ search: text, page: "1" }, { replace: true })}
						textSearch={search}
					/>
				}
			>
				<h1>Flights</h1>
				{flights.map((flight: IFlight, index: number) => (
					<Card key={index} style={{ marginBottom: "20px" }}>
						<Grid container spacing={2}>
							<Grid item xs={12} sm={6}>
								<CardMedia
									component="img"
									alt="Flight Image"
									style={{objectFit: "cover", height: `${getImageSize()}px`, width: "100%"}}
									image={flight.image || ""}
									title="Flight Image"
								/>
							</Grid>
							<Grid item xs={12} sm={6}>
								<CardContent>
									<Typography gutterBottom variant="h4" component="h2">
										{flight.airline}
									</Typography>
									<Typography variant="body2" color="textSecondary" component="p">
		    Departure: {flight.departureTime ? new Date(flight.departureTime).toDateString() : "N/A"}
									</Typography>
									<Typography variant="body2" color="textSecondary" component="p">
		    Arrival: {flight.arrivalTime ? new Date(flight.arrivalTime).toDateString() : "N/A"}
									</Typography>
		
									<CardActions>
										<Button
											size="small"
											color="primary"
											variant="contained"
											onClick={() => handleDetails(flight.idFlight || 1)}
										>
		                                        Detalhes
										</Button>
									</CardActions>
								</CardContent>
							</Grid>
						</Grid>
					</Card>
				))}
				<Stack spacing={4}>
				</Stack>
			</LayoutBasePage>
		</div>
	);
};
