import React, { useEffect, useState } from "react";
import { ReservationService } from "../../shared/services/api/reservation/ReservationService";
import { IReservation } from "../../shared/Interfaces/Interfaces";
import { Button, Card,  CardActions, CardContent, CardMedia, Grid, Typography } from "@mui/material";
import { useAuthContext } from "../../shared/contexts/AuthContext";
const ShoppingCart = () => {
	const [reservations, setReservations] = useState<IReservation[]>([]);
	const { user, RemoveFromCart } = useAuthContext ();

	const handleDelete = async (id: number) => {
		try {
			await ReservationService.deleteReservation(id);
			console.log(`Reservation ${id} deleted successfully.`);
			RemoveFromCart(id);
			setReservations(reservations.filter(reservation => reservation.idReservation !== id));
		} catch (error) {
			console.error(error);
			alert("Error deleting the reservation.");
		}
	};
	useEffect(() => {
		const fetchReservations = async () => {
			const userId = user?.IdUser ?? 1; // Substitua pelo ID do usuário atual
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
					<Grid container spacing={2}>
						<Grid item xs={12} sm={6}>
							<CardMedia
								component="img"
								alt="Hotel Image"
								height="170"
								image={reservation.reservedHotel?.images[0].imageUrl} // Replace with the hotel image URL
								title="Hotel Image"
							/>
						</Grid>
						<Grid item xs={12} sm={6}>
							<CardContent>
								<Typography gutterBottom variant="h5" component="h2">
									{reservation.reservedHotel?.name}
								</Typography>
								<Typography variant="body2" color="textSecondary" component="p">
            Check-in: {reservation.checkInDate}
								</Typography>
								<CardActions>
									<Button size="small" color="error" variant='contained' onClick={() => handleDelete(reservation.idReservation)}>
      Delete
									</Button>
								</CardActions>
								<CardActions>
									<Button size="small" variant='contained' onClick={() => handleDelete(reservation.idReservation)}>
      Editar
									</Button>
								</CardActions>
							</CardContent>
						</Grid>
					</Grid>

				</Card>

			))}
		</div>
	);
};

export default ShoppingCart;
