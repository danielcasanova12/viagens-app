
import { ICreateReservation,IImage,IReservation,IReservationsAll } from "../../../Interfaces/Interfaces";
import { Api } from "../axios-config";


const getReservationById = async (id: number): Promise<ICreateReservation | Error> => {
	try {
		const { data } = await Api.get(`/Reservation/${id}`);

		if (data) {
			return data;
		}

		return new Error("Erro ao obter a reserva.");
	} catch (error) {
		console.error(error);
		return new Error((error as { message: string }).message || "Erro ao obter a reserva.");
	}
};
const getReservationsByUserId = async (userId: number): Promise<IReservationsAll | Error> => {
	try {
		const { data } = await Api.get(`/Reservation/user/${userId}`);
		console.log("data",data);
		if (data) {
			const reservations = data.reservations.map((reservation: IReservation) => {
				let teste = null;

				if (reservation.reservedHotel) {
					teste =  {
						idReservation: reservation.idReservation,
						userId: reservation.userId,
						checkInDate: reservation.checkInDate,
						checkOutDate: reservation.checkOutDate,
						reservedHotel: {
							idHotel: reservation.reservedHotel.idHotel,
							name: reservation.reservedHotel.name,
							location: reservation.reservedHotel.location,
							starRating: reservation.reservedHotel.starRating,
							pricePerNight: reservation.reservedHotel.pricePerNight,
							images: reservation.reservedHotel.images ? reservation.reservedHotel.images.map((image: IImage) => ({
								id: image.id,
								hotelId: image.hotelId,
								imageUrl: image.imageUrl,
							})) : [],
						}
						
					};
					console.log("teste", teste);	
				} else if (reservation.carRentals) {
					teste =  {
						idReservation: reservation.idReservation,
						userId: reservation.userId,
						checkInDate: reservation.checkInDate,
						checkOutDate: reservation.checkOutDate,
						carRentals: {
							idCarRental: reservation.carRentals?.idCarRental,
							company: reservation.carRentals?.company,
							model: reservation.carRentals?.model,
							pricePerDay: reservation.carRentals?.pricePerDay,
							image: reservation.carRentals?.image,
							pickupLocation: reservation.carRentals?.pickupLocation
						}
					};	
				} else if (reservation.flights) {
					teste =  {
						idReservation: reservation.idReservation,
						userId: reservation.userId,
						checkInDate: reservation.checkInDate,
						checkOutDate: reservation.checkOutDate,
						flights: {
							idFlight: reservation.flights.idFlight,
							airline: reservation.flights.airline,
							departureLocation: reservation.flights.departureLocation || {},
							arrivalLocation: reservation.flights.arrivalLocation || {},
							departureTime: reservation.flights.departureTime,
							arrivalTime: reservation.flights.arrivalTime,
							image: reservation.flights.image,
							price: reservation.flights.price
						}
					};	
				}
				return teste;
			});

			const teste2 = {
				reservations,
				totalReservations: data.totalReservations,
			};
			console.log("teste2", teste2);
			return teste2;
		}

		return new Error("Erro ao obter as reservas.");
	} catch (error) {
		console.error(error);
		return new Error((error as { message: string }).message || "Erro ao obter as reservas.");
	}
};

const postReservation = async (reservationDto: ICreateReservation): Promise<ICreateReservation | Error> => {
	try {
		const { data } = await Api.post("/Reservation", reservationDto);

		if (data) {

			console.log("data", data);
			
		}

		return new Error("Erro ao criar a reserva.");
	} catch (error) {
		console.error(error);
		return new Error((error as { message: string }).message || "Erro ao criar a reserva.");
	}
};

export const deleteReservation = async (id: number): Promise<void> => {
	try {
		await Api.delete(`/Reservation/${id}`);
	} catch (error) {
		console.error(error);
		throw new Error((error as { message: string }).message || "Error deleting the reservation.");
	}
};

export const ReservationService = {
	getReservationById,
	getReservationsByUserId,
	postReservation,
	deleteReservation,
};