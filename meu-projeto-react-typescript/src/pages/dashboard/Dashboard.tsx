import React from "react";
import { LayoutBasePage } from "../../shared/layouts";
import { Toolbar } from "../../shared/components";

export const Dashboard = () => {
	return (
		<LayoutBasePage
			title='Dashboard'
			toolbar={
				<Toolbar 
					showInputSearch={true}
					textNewButton='Nova'
				></Toolbar>
			}>
			Testes
		</LayoutBasePage>
	);
};