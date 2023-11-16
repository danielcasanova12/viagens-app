import React, { useEffect, useState } from "react";
import { LayoutBasePage } from "../../shared/layouts";
import { useParams } from "react-router-dom";
import { HotelService} from "../../shared/services/api";
import Box from "@mui/material/Box";
import Typography from "@mui/material/Typography";
import IconButton from "@mui/material/IconButton";
import ArrowBackIosIcon from "@mui/icons-material/ArrowBackIos";
import ArrowForwardIosIcon from "@mui/icons-material/ArrowForwardIos";
import useMediaQuery from "@mui/material/useMediaQuery";
import { useTheme } from "@mui/material/styles";
import { Button } from "@mui/material";
import AddShoppingCartIcon from "@mui/icons-material/AddShoppingCart";
import {useAuthContext} from "../../shared/contexts/AuthContext";
import { IHotel, IReservation } from "../../shared/Interfaces/Interfaces";





export const HotelDetails = () => {
	const { id } = useParams();
	const { AddToCart } = useAuthContext();
	const [hotel, setHotel] = useState<IHotel | null>(null);
	const [currentImageIndex, setCurrentImageIndex] = useState(0);
	const theme = useTheme();
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

	useEffect(() => {
		const fetchHotel = async () => {
			const result = await HotelService.getHotelById(Number(id));
			if (!(result instanceof Error)) {
				setHotel(result);

			}
		};

		fetchHotel();
	}, [id]);

	const handlePreviousImage = () => {
		setCurrentImageIndex((prevIndex) => prevIndex - 1);
	};

	const handleNextImage = () => {
		setCurrentImageIndex((prevIndex) => prevIndex + 1);
	};

	const handleAdd = () => {
		console.log("asd");
		const newReservation: IReservation = {
			IdReservation: 1, // Substitua por seus próprios dados
			date: "2023-11-15", // Substitua por seus próprios dados
			time: "20:00", // Substitua por seus próprios dados
			IdUser: 123, // Substitua por seus próprios dados
			ReservedHotel: hotel as IHotel, // Substitua por seus próprios dados
		};
		console.log(newReservation);
		AddToCart(newReservation);
	};
	
	

	return (
		<LayoutBasePage title="Detalhes do Hotel">
			{hotel ? (
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
							<ArrowBackIosIcon />
						</IconButton>
						<img src={hotel.images[currentImageIndex].imageUrl} alt={hotel.name} style={{ maxHeight: getImageSize(), width: "auto" }} />
						<IconButton onClick={handleNextImage} disabled={currentImageIndex === hotel.images.length - 1}>
							<ArrowForwardIosIcon />
						</IconButton>
					</Box>
					<Typography variant="h4" component="h2" gutterBottom>
						{hotel.name}
					</Typography>
					<Typography variant="body1" gutterBottom>
						Localização: {hotel.location?.city}, {hotel.location?.state}, {hotel.location?.country}
					</Typography>
					<Typography variant="body1" gutterBottom>
						Preço por noite: R${hotel.pricePerNight}
					</Typography>
					<Typography variant="body1" gutterBottom>
						Avaliação: {hotel.starRating} estrelas
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
				<p>Carregando detalhes do hotel...</p>
			)}
		</LayoutBasePage>
	);
};