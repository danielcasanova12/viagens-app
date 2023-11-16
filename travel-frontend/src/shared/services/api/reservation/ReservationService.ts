
import { IReservation } from "../../../Interfaces/Interfaces";
import { Api } from "../axios-config";

const postReservation = async (reservationDto: IReservation): Promise<IReservation | Error> => {
	try {
		const { data } = await Api.post(`/reservations`, reservationDto);

		if (data) {
			console.log(data);
			
		}

		return new Error("Erro ao criar a reserva.");
	} catch (error) {
		console.error(error);
		return new Error((error as { message: string }).message || "Erro ao criar a reserva.");
	}
};

export const ReservationService = {
	postReservation,
};