import { Routes, Route, Navigate } from "react-router-dom";
import { Dashboard } from "../pages";
import React from "react";
export const AppRoutes = () => {
	return (
		<Routes>
			<Route path="/Dashboard" element={<Dashboard />} />
			<Route path="*" element={<Navigate to="/Dashboard" />} />
		</Routes>
	);
};