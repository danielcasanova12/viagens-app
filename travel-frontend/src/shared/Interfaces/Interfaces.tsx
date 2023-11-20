export interface IReservation {
	idReservation: number;
  userId: number;
	checkInDate?: string;
  checkOutDate?: string;
	reservedHotel?: IHotel;
  carRentals?: ICarRental;
  flights?: IFlight;
}
export interface IReservationsAll  {
  reservations: IReservation[];
	totalReservations: number;
}
export interface IImage {
	id: number;
  hotelId: number;
  imageUrl: string;
}
export interface IHotel {
  idHotel: number;
  name: string;
  location?: ILocation;
  starRating: number;
  pricePerNight: number;
	images: IImage[];
}
export interface ILocation{
    idLocal: number;
    name: string;
    adress: string;
    city: string;
    state: string;
    country: string;
    image?: string;
}

export interface ICreateReservation {
  UserId: number;
	checkInDate?: string;
  checkOutDate?: string;
	reservedHotel?: IHotel;
  CarRentals?: ICarRental;
  Flights?: IFlight;
}


export interface IUser {
	IdUser: number;
  username: string;
  email : string;
  password: string;
  image: string;
  typePermission: string;
  reservations?: IReservation[]; // Add this line
}
export interface IUsers {
    username: string;
    email : string;
    password: string;
    image: string;
    typePermission: string;
    reservations?: IReservation[]; // Add this line
}
export interface IAuth {
    accessToken: string;
    user: IUser; // Adicione esta linha
}
export interface ICarRental {
  idCarRental: number | null;
  company: string | null;
  model: string | null;
  pricePerDay: number | null;
  image: string | null;
  pickupLocation: ILocation | null;
}

export interface IFlight {
  idFlight: number | null;
  airline: string | null;
  departureLocation: ILocation | null;
  arrivalLocation: ILocation | null;
  departureTime: Date | null;
  arrivalTime: Date | null;
  image: string | null;
  price: number | null;
}