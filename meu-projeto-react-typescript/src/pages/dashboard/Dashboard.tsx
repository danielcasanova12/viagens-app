import React from "react";
import { LayoutBasePage } from "../../shared/layouts";
import { ToolsDetails } from "../../shared/components";

export const Dashboard = () => {
	return (
		<LayoutBasePage
			title='Dashboard'
			toolbar={
				<ToolsDetails />
			}>
			Testes
		</LayoutBasePage>
	);
};