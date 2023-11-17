import React, { useEffect, useState } from "react";
import { ReservationService } from "../../shared/services/api/reservation/ReservationService";
import { IReservation } from "../../shared/Interfaces/Interfaces";
import { Card, CardActionArea, CardContent, CardMedia, Typography } from "@mui/material";

const ShoppingCart = () => {
	const [reservations, setReservations] = useState<IReservation[]>([]);
  
	useEffect(() => {
		const fetchReservations = async () => {
			const userId = 1; // Substitua pelo ID do usuário atual
			const result = await ReservationService.getReservationsByUserId(userId);
			if (result && "reservations" in result && Array.isArray(result.reservations)) {
				setReservations( result.reservations );
				console.log(result.reservations[0].reservedHotel?.images[0].imageUrl);
			} else {
				console.error(result);
			}
		};

		fetchReservations();
	}, []);

	return (
		<div>
			<h1>Shopping Cart</h1>
			{reservations.map((reservation, index) => (
				    <Card key={index} style={{ marginBottom: "20px" }}>
					<CardActionArea>
						<CardMedia
							component="img"
							alt="Hotel Image"
							height="140"
							image={reservation.reservedHotel?.images[0].imageUrl} // Substitua pela URL da imagem do hotel
							title="Hotel Image"
						/>
						<CardContent>
							<Typography gutterBottom variant="h5" component="h2">
									Reserva {index + 1}
							</Typography>
							<Typography variant="body2" color="textSecondary" component="p">
									Check-in: {reservation.checkInDate}
							</Typography>
							{/* Adicione mais detalhes do hotel aqui */}
						</CardContent>
					</CardActionArea>
				</Card>
			))}
		</div>
	);
};

export default ShoppingCart;
