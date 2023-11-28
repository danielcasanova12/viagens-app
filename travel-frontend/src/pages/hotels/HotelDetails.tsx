import React, { useEffect, useState } from "react";
import { LayoutBasePage } from "../../shared/layouts";
import { useNavigate, useParams } from "react-router-dom";
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
import { ICreateReservation, IHotel, IReservation } from "../../shared/Interfaces/Interfaces";
import {ReservationService}  from "../../shared/services/api/reservation/ReservationService";
import { Dialog, DialogTitle, DialogContent, DialogContentText, DialogActions} from "@mui/material";
import CheckCircleIcon from "@mui/icons-material/CheckCircle";
import { green } from "@mui/material/colors";

import dayjs, { Dayjs } from "dayjs";
import "dayjs/locale/de";
import "dayjs/locale/en-gb";
import "dayjs/locale/zh-cn";
import Stack from "@mui/material/Stack";
import ToggleButton from "@mui/material/ToggleButton";
import ToggleButtonGroup from "@mui/material/ToggleButtonGroup";
import { AdapterDayjs } from "@mui/x-date-pickers/AdapterDayjs";
import { DateField } from "@mui/x-date-pickers/DateField";
import { LocalizationProvider } from "@mui/x-date-pickers/LocalizationProvider";

const locales = ["en", "en-gb", "zh-cn", "de"];

type LocaleKey = (typeof locales)[number];

export const HotelDetails = () => {
	const { user} = useAuthContext ();
	const navigate = useNavigate();
	const { id } = useParams();
	const { AddToCart } = useAuthContext();
	const [hotel, setHotel] = useState<IHotel | null>(null);
	const [currentImageIndex, setCurrentImageIndex] = useState(0);
	const theme = useTheme();
	const smDown = useMediaQuery(theme.breakpoints.down("sm"));
	const mdDown = useMediaQuery(theme.breakpoints.down("md"));
	const { postReservation } = ReservationService;
	const [open, setOpen] = useState(false);
	const [datecheckIn, setDatecheckIn] = React.useState<Dayjs | null>(dayjs());
	const [datecheckOut, setDatecheckOut] = React.useState<Dayjs | null>(dayjs());
	const [locale, setLocale] = React.useState<LocaleKey>("en");
	const [checkInError, setCheckInError] = useState(false);
	const [checkOutError, setCheckOutError] = useState(false);
	

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
		const a = datecheckIn?.toISOString();
		console.log("a: ", a);
		const newReservation: ICreateReservation = {
			UserId: user?.idUser ?? 2,
			checkInDate:datecheckIn?.toISOString()|| "2023-12-17T00:13:15.719Z",
			checkOutDate:datecheckOut?.toISOString()|| "2023-12-17T00:13:15.719Z", 
			reservedHotel: hotel as IHotel,
			Confirmed: false
		};
		postReservation(newReservation);
		const newReservation2: IReservation = {
			idReservation: 1,
			checkInDate:datecheckIn?.toISOString()|| "2023-12-17T00:13:15.719Z",
			checkOutDate:datecheckOut?.toISOString()|| "2023-12-17T00:13:15.719Z",
			userId: user?.idUser ?? 2,
			reservedHotel: hotel as IHotel,
			confirmed: false
		};
		AddToCart(newReservation2,user?.idUser ?? 2);
		setOpen(true);
	};
	const handleClose = () => {
		setOpen(false);
	};
	const goToCart = () => {
		navigate("/shoppingCart");
	};

	const handleDateChangeCheckIn = (newValue: Dayjs | null) => {
		if (newValue) {
			const today =  dayjs();
	
			if (newValue >= today) {
				setDatecheckIn(newValue);
				setCheckInError(false); 
			} else {
				setCheckInError(true);
				console.error("A data de check-in não pode ser anterior à data atual.");
			}
		}
	};
	
	const handleDateChangeCheckOut = (newValue: Dayjs | null) => {
		if (newValue && datecheckIn && newValue > datecheckIn) {
			setDatecheckOut(newValue);
			setCheckOutError(false); 
		} else {
			setCheckOutError(false); 
			console.error("A data de check-out deve ser posterior à data de check-in.");
		}
	};
	return (
		<Box >
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
          	<Box >
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
						<Box   sx={{
							display: "flex",
							flexDirection: "column",
							alignItems: "center",
							justifyContent: "center",
						}}>
							<LocalizationProvider dateAdapter={AdapterDayjs} adapterLocale={locale}>
								<Stack spacing={3} sx={{ width: 300 }}>
									<ToggleButtonGroup
										value={locale}
										exclusive
										fullWidth
										onChange={(event, newLocale) => {
											if (newLocale != null) {
												setLocale(newLocale);
											}
										}}
									>
										{locales.map((localeItem) => (
											<ToggleButton key={localeItem} value={localeItem}>
												{localeItem}
											</ToggleButton>
										))}
									</ToggleButtonGroup>
									<DateField
										label="CheckIn Date"
										value={datecheckIn}
										onChange={handleDateChangeCheckIn}
									/>
									{checkInError && (
										<Typography variant="body2" color="error">
											A data de check-in não pode ser anterior à data atual.
										</Typography>
									)}
									<DateField
										label="CheckOut Date"
										value={datecheckOut}
										onChange={handleDateChangeCheckOut}
									/>
									{checkOutError && (
										<Typography variant="body2" color="error">
											A data de check-out deve ser posterior à data de check-in.
										</Typography>
									)}
								</Stack>
							</LocalizationProvider>
						</Box>
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
				<Dialog open={open} onClose={handleClose}>
					<DialogTitle>{"Sucesso!"}</DialogTitle>
					<DialogContent>
						<DialogContentText style={{ color: green[900] }}>
							<CheckCircleIcon /> Hotel adicionado ao carrinho com sucesso!
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
		</Box>
	);
};


