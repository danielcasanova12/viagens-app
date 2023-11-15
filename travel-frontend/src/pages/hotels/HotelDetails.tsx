import React, { useEffect, useState } from "react";
import { LayoutBasePage } from "../../shared/layouts";
import { useParams } from "react-router-dom";
import { HotelService, IHotel } from "../../shared/services/api";
import ImageList from "@mui/material/ImageList";
import ImageListItem from "@mui/material/ImageListItem";
import Button from "@mui/material/Button";
import Box from "@mui/material/Box";
import Typography from "@mui/material/Typography";
import AddShoppingCartIcon from "@mui/icons-material/AddShoppingCart";
import IconButton from "@mui/material/IconButton";

function srcset(image: string, size: number, rows = 1, cols = 1) {
	return {
		src: `${image}?w=${size * cols}&h=${size * rows}&fit=crop&auto=format`,
		srcSet: `${image}?w=${size * cols}&h=${
			size * rows
		}&fit=crop&auto=format&dpr=2 2x`,
	};
}

export const HotelDetails = () => {
	const { id } = useParams();
	const [hotel, setHotel] = useState<IHotel | null>(null);

	useEffect(() => {
		const fetchHotel = async () => {
			const result = await HotelService.getHotelById(Number(id));
			if (!(result instanceof Error)) {
				setHotel(result);
			}
		};

		fetchHotel();
	}, [id]);

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
					<ImageList
						sx={{ width: "100%" }}
						variant="quilted"
						cols={4}
						rowHeight={121}
					>
						{hotel.images.map((image) => (
							<ImageListItem key={image.id} cols={1} rows={1}>
								<img
									{...srcset(image.imageUrl, 121, 1, 1)}
									alt={hotel.name}
									loading="lazy"
								/>
							</ImageListItem>
						))}
					</ImageList>
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
					
					<Button variant="contained" color="secondary" sx={{ mt: 2 }}>
						<IconButton color="primary" aria-label="add to shopping cart">
							<AddShoppingCartIcon />
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
