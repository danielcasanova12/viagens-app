import { Routes, Route } from "react-router-dom";
import { Carros, Dashboard, Hotels, HotelDetails } from "../pages";
import React from "react";
import { Profile } from "../pages/profile/Profile";
import ShoppingCart from "../pages/shoppingCart/ShoppingCart";

export const AppRoutes = () => {
	return (
		<Routes>
			<Route path="/Dashboard" element={<Dashboard />} />
			<Route path="/Hotels" element={<Hotels />} />
			<Route path="/carros" element={<Carros/>} /> 
			<Route path="/profile" element={<Profile/>} /> 
			<Route path="/Hotels/detalhes/:id" element={<HotelDetails />} />
			<Route path="/shoppingCart" element={<ShoppingCart/>} /> 
			{/* <Route path="*" element={<Navigate to="/Dashboard" />} /> */}
		</Routes>
	);
};