import React, { useEffect, useState } from "react";
import { LayoutBasePage } from "../../shared/layouts";
import { useNavigate, useParams } from "react-router-dom";
import Box from "@mui/material/Box";
import Typography from "@mui/material/Typography";
import IconButton from "@mui/material/IconButton";
import { Button, useMediaQuery, useTheme } from "@mui/material";
import AddShoppingCartIcon from "@mui/icons-material/AddShoppingCart";
import {useAuthContext} from "../../shared/contexts/AuthContext";
import { ICreateReservation, IFlight, IReservation } from "../../shared/Interfaces/Interfaces";
import {ReservationService}  from "../../shared/services/api/reservation/ReservationService";
import { Dialog, DialogTitle, DialogContent, DialogContentText, DialogActions} from "@mui/material";
import CheckCircleIcon from "@mui/icons-material/CheckCircle";
import { green } from "@mui/material/colors";
import { FlightService } from "../../shared/services/api/flights/FlightsService";
export const FlightDetails = () => {
	const { user} = useAuthContext ();
	const navigate = useNavigate();
	const { id } = useParams();
	const { AddToCart } = useAuthContext();
	const [flight, setFlight] = useState<IFlight | null>(null);
	const { postReservation } = ReservationService;
	const [open, setOpen] = useState(false);
	const theme = useTheme();
	const smDown = useMediaQuery(theme.breakpoints.down("sm"));
	const mdDown = useMediaQuery(theme.breakpoints.down("md"));

	useEffect(() => {
		const fetchFlight = async () => {
			const result = await FlightService.getFlightById(Number(id));
			if (!(result instanceof Error)) {
				setFlight(result);
			}
		};

		fetchFlight();
	}, [id]);

	const handleAdd = () => {
		const newReservation: ICreateReservation = {
			UserId: user?.idUser ?? 2,
			checkInDate: "2023-12-17T00:13:15.719Z",
			checkOutDate:"2023-12-17T00:13:15.719Z", 
			Flights: flight as IFlight,
			Confirmed: false
		};
		postReservation(newReservation);
		const newReservation2: IReservation = {
			idReservation: 1,
			checkInDate: "2023-11-16T23:01:34.320Z",
			checkOutDate: "2023-11-16T23:01:34.320Z", 
			userId: user?.idUser ?? 2,
			flights: flight as IFlight,
			confirmed: false
		};
		AddToCart(newReservation2,1);
		setOpen(true);
	};
	const handleClose = () => {
		setOpen(false);
	};
	const goToCart = () => {
		navigate("/shoppingCart");
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
	return (
		<LayoutBasePage title="Detalhes do Voo">
			{flight ? (
				<Box
					sx={{
						display: "flex",
						flexDirection: "column",
						alignItems: "center",
						width: "100%",
						maxWidth: 500,
						margin: "0 auto",
						padding: 2,
					}}
				>
					<Box sx={{ display: "flex", alignItems: "center" }}>
						<img src={flight.image || ""} alt={flight.airline || ""} style={{ maxHeight: getImageSize(), width: "auto" }} />
					</Box>
					<Typography variant="h4" component="h2" gutterBottom>
						{flight.airline}
					</Typography>
					<Typography variant="body1" gutterBottom>
						Localização saida: {flight.departureLocation?.city}, {flight.departureLocation?.state}, {flight.departureLocation?.country}
					</Typography>
					<Typography variant="body1" gutterBottom>
						Localização chegada: {flight.arrivalLocation?.city}, {flight.arrivalLocation?.state}, {flight.arrivalLocation?.country}
					</Typography>
					<Typography variant="body1" gutterBottom>
						Preço da passagem: R${flight.price}
					</Typography>
					<Button 
						variant="contained" 
						color="primary" 
						sx={{ mt: 2 }} 
						
						onClick={() => handleAdd()} // Substitua "item" pelos seus próprios dados
					>
						<IconButton>
							<AddShoppingCartIcon/>
						</IconButton>
  Adicionar ao carrinho
					</Button>
				</Box>
			) : (
				<p>Carregando detalhes do voo...</p>
			)}
			<Dialog open={open} onClose={handleClose}>
				<DialogTitle>{"Sucesso!"}</DialogTitle>
				<DialogContent>
					<DialogContentText style={{ color: green[900] }}>
						<CheckCircleIcon /> Voo adicionado ao carrinho com sucesso!
					</DialogContentText>
				</DialogContent>
				<DialogActions>
					<Button onClick={goToCart}  color="secondary" autoFocus >
            Ver Carrinho
					</Button>
					<Button onClick={handleClose}  variant="contained" color="success" autoFocus>
            OK
					</Button>
				</DialogActions>
			</Dialog>
		</LayoutBasePage>
	);
};
