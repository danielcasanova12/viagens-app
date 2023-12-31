import React, { useEffect, useState } from "react";
import { ReservationService } from "../../shared/services/api/reservation/ReservationService";
import { IReservation } from "../../shared/Interfaces/Interfaces";
import { Box, Button, Card,  CardActions, CardContent, CardMedia, Container, Dialog, DialogActions, DialogContent, DialogContentText, DialogTitle, Grid, Typography } from "@mui/material";
import { useAuthContext } from "../../shared/contexts/AuthContext";
import { useTheme } from "@mui/material/styles";
import useMediaQuery from "@mui/material/useMediaQuery";
import { green, red } from "@mui/material/colors";

const ShoppingCart = () => {
	const [reservations, setReservations] = useState<IReservation[]>([]);
	const { user, RemoveFromCart,RemoveAllFromCart} = useAuthContext ();
	const theme = useTheme();
	const smDown = useMediaQuery(theme.breakpoints.down("sm"));
	const mdDown = useMediaQuery(theme.breakpoints.down("md"));
	const [open, setOpen] = useState(false);
	const [concluido, setConcluido] = useState(false);
	const [total, setTotal] = useState(0);

	const calculateTotal = (valor : number) => {

		setTotal(totalPrevio => totalPrevio + valor);
	};

	const handleConfirmOrder = async () => {
		try{
			ReservationService.putReservationStatus(user?.idUser ?? 1);
			
			setConcluido(true);
			setTotal(0);
			console.log("Total: ",total);
			localStorage.removeItem("APP_CART");

		}catch(error){
			console.log(error);
		}

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
	const handleCloseAndFetch = async () => {
		RemoveAllFromCart(user?.idUser ?? 1);
		handleClose();
		await fetchReservations();
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
		setConcluido(false);
	};
	const CalculaTotal = () => {
		console.log("reservationQWEQWE");
		setTotal(0);
		reservations.map((reservation) => {
			console.log("reservation", reservation);
			 
			const valor = reservation.flights?.price || reservation.reservedHotel?.pricePerNight || reservation.carRentals?.pricePerDay || 0;
			console.log("valor",valor);
			calculateTotal(valor);
		});
		console.log("total: ",total);
	
	};
	const fetchReservations = async () => {
		const userId = user?.idUser ?? 1; // Substitua pelo ID do usuário atual
		const result = await ReservationService.getReservationsByUserId(userId);
		if (result && "reservations" in result && Array.isArray(result.reservations)) {
			setReservations( result.reservations );
		}
		else{
			setReservations([]);
		}	
	};
	useEffect(() => {
		if(reservations.length > 0){
			CalculaTotal();
		}
	}, [reservations]);
	
	useEffect(() => {
		fetchReservations();
	}, []);


	return (
		<Container  maxWidth="xl">
			<h1>Shopping Cart</h1>
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
										{reservation && (reservation.flights
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

								<Dialog open={concluido} onClose={handleClose}>
									<DialogTitle>{"Reserva concluída"}</DialogTitle>
									<DialogContent>
										<DialogContentText style={{ color: green[900] }}>
										Reserva concluida com sucesso!
										</DialogContentText>
									</DialogContent>
									<DialogActions>
										<Button onClick={handleCloseAndFetch} variant="contained" color="primary" autoFocus>
  Ok
										</Button>
									
									</DialogActions>
								</Dialog>
							</Grid>
						</Grid>
					
					</Card>
				</Box>
			))}
			{reservations.length > 0 && (
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
			)}
			{reservations.length == 0 && (
				<Typography variant="h4" component="div" gutterBottom>
					Adicione reservas ao seu carrinho para ver o total do pedido.
				</Typography>
			)}
		</Container>
	);
};
export default ShoppingCart;
