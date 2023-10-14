import React from "react";
import { LayoutBasePage } from "../../shared/layouts";
import { ToolsList } from "../../shared/components";

export const Dashboard = () => {
	return (
		<LayoutBasePage
			title='Dashboard'
			toolbar={
				<ToolsList 
					showInputSearch={true}
					textNewButton='Nova'
				></ToolsList>
			}>
			Testes
		</LayoutBasePage>
	);
};