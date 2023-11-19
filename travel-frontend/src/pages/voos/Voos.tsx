// import React, { useEffect, useState } from "react";
// import { FlightService } from "../../shared/services/api/flights/FlightsService";  // Importe o FlightService
// import { Card, Grid, CardMedia, CardContent, Typography, CardActions, Button } from "@material-ui/core";
// import { IFlight } from "../../shared/Interfaces/Interfaces";
// import { useTheme } from "@mui/material/styles";
// import { LayoutBasePage } from "../../shared/layouts/LayoutBasePage";
// import { ToolsList } from "../../shared/components";
// export const Voos = () => {
// 	const [flights, setFlights] = useState<IFlight[]>([]);
// 	const [searchValue, setSearchValue] = useState("");
// 	const [pageNumber, setPageNumber] = useState(1);
// 	const theme = useTheme();



// 	const getBackgroundColor = () => {
// 		// Use o tema aqui para ajustar a cor de fundo com base no tema atual
// 		return theme.palette.background.default;
// 	};
// 	useEffect(() => {
// 		const fetchFlights = async () => {
// 			const result = await FlightService.getAllFlights(searchValue, pageNumber);
// 			if (result instanceof Error) {
// 				console.error(result);
// 			} else {
// 				setFlights(result[0].flighti);
// 			}
// 		};

// 		fetchFlights();
// 	}, [searchValue, pageNumber]); // Chame fetchFlights sempre que searchValue ou pageNumber mudar

// 	const handleDetails = (id: number) => {
// 		// Implemente esta função para lidar com a ação quando o botão "Detalhes" é clicado
// 		console.log(`Detalhes do voo ${id}`);
// 		setSearchValue("");
// 		setPageNumber(1);
// 		getBackgroundColor();
// 	};

// 	const getImageSize = () => {
// 		// Implemente esta função para retornar o tamanho da imagem
// 	};

// 	return (
// 		<div>
// 			 			<LayoutBasePage
// 				title="Car Rentals"
// 				toolbar={
// 					<ToolsList
// 						showInputSearch={true}
// 						showSaveButton={false}
// 					/>
// 				}
// 			>
// 				<h1>Flights</h1>
// 				{flights.map((flight: IFlight, index: number) => (
// 					<Card key={index} style={{ marginBottom: "20px" }}>
// 						<Grid container spacing={2}>
// 							<Grid item xs={12} sm={6}>
// 								<CardMedia
// 									component="img"
// 									alt="Flight Image"
// 									style={{objectFit: "cover", height: `${getImageSize()}px`, width: "100%"}}
// 									image={flight.image || ""}
// 									title="Flight Image"
// 								/>
// 							</Grid>
// 							<Grid item xs={12} sm={6}>
// 								<CardContent>
// 									<Typography gutterBottom variant="h4" component="h2">
// 										{flight.airline}
// 									</Typography>
// 									<Typography variant="body2" color="textSecondary" component="p">
//     Departure: {flight.departureTime ? new Date(flight.departureTime).toDateString() : "N/A"}
// 									</Typography>
// 									<Typography variant="body2" color="textSecondary" component="p">
//     Arrival: {flight.arrivalTime ? new Date(flight.arrivalTime).toDateString() : "N/A"}
// 									</Typography>

// 									<CardActions>
// 										<Button
// 											size="small"
// 											color="primary"
// 											variant="contained"
// 											onClick={() => handleDetails(flight.idFlight || 1)}
// 										>
//                                         Detalhes
// 										</Button>
// 									</CardActions>
// 								</CardContent>
// 							</Grid>
// 						</Grid>
// 					</Card>
// 				))}
// 			</LayoutBasePage>
// 		</div>
// 	);
// };

// export default Voos;
import { useState,useEffect } from "react";
import { ToolsList } from "../../shared/components/";
import { LayoutBasePage } from "../../shared/layouts";
import { Button, Card, CardActions, CardContent, CardMedia, Grid, Typography, useTheme } from "@mui/material";
import { FlightService } from "../../shared/services/api/flights/FlightsService";
import { IFlight } from "../../shared/Interfaces/Interfaces";

export const Voos = () => {

	const [flights, setFlights] = useState<IFlight[]>([]);
	const [searchValue, setSearchValue] = useState("");
	const [pageNumber, setPageNumber] = useState(1);
	const theme = useTheme();
	
	
	
	const getBackgroundColor = () => {
		// Use o tema aqui para ajustar a cor de fundo com base no tema atual
		return theme.palette.background.default;
	};
	useEffect(() => {
		const fetchFlights = async () => {
			const result = await FlightService.getAllFlights(searchValue, pageNumber);
			if (result instanceof Error) {
				console.error(result);
			} else {
				setFlights(result[0].flighti);
			}
		};
	
		fetchFlights();
	}, [searchValue, pageNumber]); // Chame fetchFlights sempre que searchValue ou pageNumber mudar
	
	const handleDetails = (id: number) => {
		// Implemente esta função para lidar com a ação quando o botão "Detalhes" é clicado
		console.log(`Detalhes do voo ${id}`);
		setSearchValue("");
		setPageNumber(1);
		getBackgroundColor();
	};
	
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
			</LayoutBasePage>
		</div>
	);
};
