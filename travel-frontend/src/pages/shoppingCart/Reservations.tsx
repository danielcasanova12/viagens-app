import React, { useEffect, useState } from "react";
import { ReservationService } from "../../shared/services/api/reservation/ReservationService";
import { IReservation } from "../../shared/Interfaces/Interfaces";
import { Box,  Card,  CardContent, CardMedia, Container, Grid, Typography } from "@mui/material";
import { useTheme } from "@mui/material/styles";
import useMediaQuery from "@mui/material/useMediaQuery";
import { useAuthContext } from "../../shared/contexts";

const Reservation = () => {
	const [reservations, setReservations] = useState<IReservation[]>([]);
	const theme = useTheme();
	const { user } = useAuthContext();
	const smDown = useMediaQuery(theme.breakpoints.down("sm"));
	const mdDown = useMediaQuery(theme.breakpoints.down("md"));




	const getImageSize = () => {
		if (smDown) {
			return 100;
		} else if (mdDown) {
			return 350;
		} else {
			return 450;
		}
	};




	const fetchReservations = async () => {
		const userId = user?.idUser ?? 1; // Substitua pelo ID do usuário atual
		const result = await ReservationService.getReservationsAllByUserId(userId);
		if (result && "reservations" in result && Array.isArray(result.reservations)) {
			setReservations( result.reservations );
		}
		else{
			setReservations([]);
		}	
	};

	
	useEffect(() => {
		fetchReservations();
	}, []);


	return (
		<Container  maxWidth="xl">
			<h1>Suas reservas realizadas</h1>
			{reservations.map((reservation, index) => (
				<Box>
					<Card key={index} style={{ marginBottom: "20px" }}>
						<Grid container spacing={2}>
							<Grid item xs={12} sm={6}>
								<CardMedia
									component="img"
									alt="Reserved Item Image"
									style={{objectFit: "cover", height: `${getImageSize()}px`, width: "100%"}}
									image={
										(reservation && (reservation.reservedHotel
											? reservation.reservedHotel.images[0].imageUrl
											: reservation.carRentals
												? reservation.carRentals.image
												: reservation.flights
													? reservation.flights.image
													: "")) || ""
									}
									title="Reserved Item Image"
								/>
							</Grid>
							<Grid item xs={12} sm={6}>
								<CardContent>
									<Typography gutterBottom variant="h4" component="h2">
										{reservation && (reservation.reservedHotel
											? reservation.reservedHotel.name
											: reservation.carRentals
												? reservation.carRentals.company
												: reservation.flights
													? reservation.flights.airline
													: "")}
									</Typography>
									<Typography gutterBottom variant="h4" component="h2">
										{reservation && (reservation.reservedHotel
											? reservation.reservedHotel.location?.city
											: reservation.carRentals
												? reservation.carRentals.pickupLocation?.city
												: reservation.flights
													? reservation.flights.arrivalLocation?.city && reservation.flights.departureLocation?.city
													: "")}
									</Typography>
									<Typography gutterBottom variant="h4" component="h2">
										{reservation && (reservation.reservedHotel
											? reservation.reservedHotel.starRating
											: reservation.carRentals
												? reservation.carRentals.model
												: reservation.flights
													? reservation.flights.price
													: "")}
									</Typography>
									<Typography gutterBottom variant="h4" component="h2">
										{reservation && (reservation.reservedHotel
											? reservation.reservedHotel.pricePerNight
											: reservation.carRentals
												? reservation.carRentals.pricePerDay
												: "")}
									</Typography>
								
								</CardContent>
							</Grid>
						</Grid>
					
					</Card>
				</Box>
			))}
		
		</Container>
	);
};
export default Reservation;
