import React, { useEffect, useState } from "react";
import { ReservationService } from "../../shared/services/api/reservation/ReservationService";
import { IReservation } from "../../shared/Interfaces/Interfaces";
import { Box, Button, Card,  CardActions, CardContent, CardMedia, Dialog, DialogActions, DialogContent, DialogContentText, DialogTitle, Grid, Typography } from "@mui/material";
import { useAuthContext } from "../../shared/contexts/AuthContext";
import { useTheme } from "@mui/material/styles";
import useMediaQuery from "@mui/material/useMediaQuery";
import { red } from "@mui/material/colors";

const ShoppingCart = () => {
	const [reservations, setReservations] = useState<IReservation[]>([]);
	const { user, RemoveFromCart } = useAuthContext ();
	const theme = useTheme();
	const smDown = useMediaQuery(theme.breakpoints.down("sm"));
	const mdDown = useMediaQuery(theme.breakpoints.down("md"));
	const [open, setOpen] = useState(false);
	const [total, setTotal] = useState(0);
	const calculateTotal = (valor : number) => {
		console.log("valor: ", valor);
		const totalPedido = total + valor;
		// Substitua esta linha pelo cálculo real do total do seu pedido
		setTotal(totalPedido);
	};

	const handleConfirmOrder = () => {
		// Substitua esta linha pela lógica real de confirmação do pedido
		console.log("Pedido confirmado!");
	};
	const getImageSize = () => {
		if (smDown) {
			return 100;
		} else if (mdDown) {
			return 350;
		} else {
			return 450;
		}
	};
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
	const handleDelet = () => {
		setOpen(true);
	};
	const handleClose = () => {
		setOpen(false);
	};
	useEffect(() => {
		const fetchReservations = async () => {
			console.log("entrou");
			const userId = user?.idUser ?? 1; // Substitua pelo ID do usuário atual
			const result = await ReservationService.getReservationsByUserId(userId);
			console.log("result: ", result);
			if (result && "reservations" in result && Array.isArray(result.reservations)) {
				setReservations( result.reservations );
			} else {
				console.error(result);
			}
			
		};

		fetchReservations();
	}, []);
	useEffect(() => {
		reservations.map((reservation) => {
			calculateTotal(reservation.flights?.price || reservation.reservedHotel?.pricePerNight || reservation.carRentals?.pricePerDay || 0);
		});
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
								
								<CardActions>
									<Button
										size="small"
										color="error"
										variant="contained"
										onClick={() => handleDelet()}
									>
										Delete
									</Button>
								</CardActions>
								
							</CardContent>
							<Dialog open={open} onClose={handleClose}>
								<DialogTitle>{"Excluir"}</DialogTitle>
								<DialogContent>
									<DialogContentText style={{ color: red[900] }}>
										Dezeja deletar esta reserva?
									</DialogContentText>
								</DialogContent>
								<DialogActions>
									<Button onClick={handleClose}  variant="contained" color="primary" autoFocus>
            Cancelar
									</Button>
									<Button onClick={() => handleDelete(reservation.idReservation)}variant="contained"  color="error" autoFocus >
            Deletar
									</Button>
									
								</DialogActions>
							</Dialog>
						</Grid>
					</Grid>
				</Card>
				
			))}
			<Box
				sx={{
					display: "flex",
					flexDirection: "column",
					alignItems: "center",
					justifyContent: "center",
					padding: 2,
				}}
			>
				<Typography variant="h4" component="div" gutterBottom>
        Total do Pedido: {total}
				</Typography>
				<Button variant="contained" onClick={handleConfirmOrder}>
        Confirmar Pedido
				</Button>
			</Box>
		</div>
	);
};
export default ShoppingCart;
