import { Routes, Route } from "react-router-dom";
import { Carros, Dashboard, Voos } from "../pages";
import React from "react";
import { Profile } from "../pages/profile/Profile";

export const AppRoutes = () => {
	return (
		<Routes>
			<Route path="/Dashboard" element={<Dashboard />} />
			<Route path="/Voos" element={<Voos />} />
			<Route path="/carros" element={<Carros/>} /> 
			<Route path="/profile" element={<Profile/>} /> 
			{/* <Route path="*" element={<Navigate to="/Dashboard" />} /> */}
		</Routes>
	);
};