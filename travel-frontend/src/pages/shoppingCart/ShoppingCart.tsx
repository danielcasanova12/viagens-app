import React, { useEffect, useState } from "react";
import { ReservationService } from "../../shared/services/api/reservation/ReservationService";
import { IReservation } from "../../shared/Interfaces/Interfaces";

const ShoppingCart = () => {
	const [reservations, setReservations] = useState<IReservation[]>([]);

	useEffect(() => {
		const fetchReservations = async () => {
			const userId = 1; // Substitua pelo ID do usuário atual
			const result = await ReservationService.getReservationsByUserId(userId);
			console.log(result);
			if (result && "reservations" in result && Array.isArray(result.reservations)) {
				setReservations( result.reservations );
				console.log(result.totalReservations);
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
