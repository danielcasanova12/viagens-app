import { useParams } from "react-router-dom";
import { useAuthContext } from "../../shared/contexts";
import { useEffect, useState } from "react";
import { ICarRental, ICreateReservation, IReservation } from "../../shared/Interfaces/Interfaces";
import { useTheme } from "@mui/material/styles";
import { Box, Button, IconButton, Typography, useMediaQuery } from "@mui/material";
import { ReservationService } from "../../shared/services/api/reservation/ReservationService";
import { CarService } from "../../shared/services/api/car/CarService";
import { LayoutBasePage } from "../../shared/layouts";
import AddShoppingCartIcon from "@mui/icons-material/AddShoppingCart";
export const CarDetails = () => {
	const { id } = useParams();
	const { AddToCart } = useAuthContext();
	const [car, setCar] = useState<ICarRental | null>(null);
	const [currentImageIndex, setCurrentImageIndex] = useState(0);
	const theme = useTheme();
	const smDown = useMediaQuery(theme.breakpoints.down("sm"));
	const mdDown = useMediaQuery(theme.breakpoints.down("md"));
	const { postReservation } = ReservationService;
	const getImageSize = () => {
		if (smDown) {
			return 100;
		} else if (mdDown) {
			return 350;
		} else {
			return 450;
		}
	};

	useEffect(() => {
		const fetchCar = async () => {
			const result = await CarService.getCarRentalById(Number(id));
			if (!(result instanceof Error)) {
				setCar(result);
			}
		};

		fetchCar();
	}, [id]);

	const handlePreviousImage = () => {
		setCurrentImageIndex((prevIndex) => prevIndex - 1);
	};

	const handleAdd = () => {
		const newReservation: ICreateReservation = {
			UserId: 1,
			checkInDate: "2023-12-17T00:13:15.719Z",
			checkOutDate:"2023-12-17T00:13:15.719Z", 
			CarRentals : car as ICarRental,
		};
		postReservation(newReservation);
		const newReservation2: IReservation = {
			idReservation: 1,
			checkInDate: "2023-11-16T23:01:34.320Z",
			checkOutDate: "2023-11-16T23:01:34.320Z", 
			userId: 1,
			carRental: car as ICarRental,
		};
		AddToCart(newReservation2,1);
		
	};
	
	

	return (
		<LayoutBasePage title="Detalhes do Carro">
			{car ? (
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
						<IconButton onClick={handlePreviousImage} disabled={currentImageIndex === 0}>
							<img src={car.image ? car.image  : undefined} alt={car.model ? car.model : undefined} style={{ maxHeight: getImageSize(), width: "auto" }} />
						</IconButton>
					</Box>
					<Typography variant="h4" component="h2" gutterBottom>
						{car.model}
					</Typography>
					<Typography variant="body1" gutterBottom>
						Companhia: {car.company}
					</Typography>
					<Typography variant="body1" gutterBottom>
						Preço por dia: R${car.pricePerDay}
					</Typography>
					<Typography variant="body1" gutterBottom>
						Localização: {car.pickupLocation?.city}, {car.pickupLocation?.state}, {car.pickupLocation?.country}
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
				<p>Carregando detalhes do carro...</p>
			)}
		</LayoutBasePage>
	);
};
