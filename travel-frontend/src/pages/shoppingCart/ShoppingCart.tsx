import React, { useEffect, useState } from "react";
import { ReservationService } from "../../shared/services/api/reservation/ReservationService";
import { IReservation } from "../../shared/Interfaces/Interfaces";

const ShoppingCart = () => {
	const [reservations, setReservations] = useState<IReservation[]>([]);

	useEffect(() => {
		const fetchReservations = async () => {
			const userId = 1; // Substitua pelo ID do usuário atual
			const result = await ReservationService.getReservationsByUserId(userId);

			if (Array.isArray(result)) {
				setReservations(result);
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
				<div key={index}>
					<h2>Reserva {index + 1}</h2>
					<p>Check-in: {reservation.checkInDate}</p>
				</div>
			))}
		</div>
	);
};

export default ShoppingCart;
