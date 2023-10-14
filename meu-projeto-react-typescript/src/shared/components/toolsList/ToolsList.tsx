import { Box, Button, Paper, TextField, useTheme} from "@mui/material";
import AddIcon from "@mui/icons-material/Add";

interface IToolsListProps {
  children?: React.ReactNode;
	textSearch?: string;
	showInputSearch?: boolean;
	changeTextSearch?: (newText: string) => void;
	textNewButton?: string;
	showNewButton?: boolean;
	onClickNewButton?: () => void;

}
export const ToolsList:React.FC<IToolsListProps> = ({
	children,
	textSearch = "",
	showInputSearch = false,
	changeTextSearch,
	textNewButton = "Novo",
	showNewButton = true,
	onClickNewButton
}) => {
	const theme = useTheme();
  
	return (
		<Box
			height={theme.spacing(5)}
			marginX={1}
			padding={1}
			paddingX={2}
			display={"flex"}
			gap={1}
			alignItems="center"
			component={Paper}
		>
			{showInputSearch && (
				<TextField
					size='small'
					placeholder='Search...'
					variant='outlined'
					value={textSearch}
					onChange={(e) => changeTextSearch?.(e.target.value)}
				/>
			)}		
			
			{ showNewButton && (
				<Box 
					flex={1}
					display={"flex"}
					justifyContent={"end"}
        
				>
					<Button
						variant='contained'
						color='primary'
						disableElevation
						endIcon={<AddIcon />}
						onClick={onClickNewButton}
					>{textNewButton}</Button>
				</Box>
			)}
			<Box>
				{children}
			</Box>
		</Box>
		
	);
};