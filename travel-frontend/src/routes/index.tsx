import { Routes, Route } from "react-router-dom";
import { Cars,  Hotels, HotelDetails, Voos } from "../pages";
import React from "react";
import { Profile } from "../pages/profile/Profile";
import ShoppingCart from "../pages/shoppingCart/ShoppingCart";
import { CarDetails } from "../pages/cars/CarsDetails";
import { FlightDetails } from "../pages/voos/VooDetails";
import Reservation from "../pages/shoppingCart/Reservations";

export const AppRoutes = () => {
	return (
		<Routes>
			<Route path="/Hotels" element={<Hotels />} />
			<Route path="/carros" element={<Cars/>} /> 
			<Route path="/profile" element={<Profile/>} /> 
			<Route path="/Hotels/detalhes/:id" element={<HotelDetails />} />
			<Route path="/car/detalhes/:id" element={<CarDetails />} />
			<Route path="/shoppingCart" element={<ShoppingCart/>} /> 
			<Route path="/voos" element={<Voos/>} /> 
			<Route path="/voos/detalhes/:id" element={<FlightDetails/>} /> 
			<Route path="/reservas" element={<Reservation/>} /> 
			{/* <Route path="*" element={<Navigate to="/Dashboard" />} /> */}
		</Routes>
	);
};