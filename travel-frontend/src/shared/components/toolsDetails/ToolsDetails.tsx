import { Box, Button, Divider, Paper, Skeleton, Theme, Typography, useMediaQuery, useTheme } from "@mui/material";
import SaveIcon from "@mui/icons-material/Save";
import DeleteIcon from "@mui/icons-material/Delete";
import AddIcon from "@mui/icons-material/Add";
import ArrowBackIcon from "@mui/icons-material/ArrowBack";

interface IToolsDetailsProps {
	textButtonNew?: string;
	hideButtonNew?: boolean;
	hideButtonBack?: boolean;
	hideButtonSave?: boolean;
	hideButtonDelete?: boolean;
	hideButtonSaveAndBack?: boolean;


	hideButtonNewLoading?: boolean;
	hideButtonBackLoading?: boolean;
	hideButtonSaveLoading?: boolean;
	hideButtonDeleteLoading?: boolean;
	hideButtonSaveAndBackLoading?: boolean;

	onclickNewButton?: () => void;
	onclickBackButton?: () => void;
	onclickSaveButton?: () => void;
	onclickDeleteButton?: () => void;
	onclickSaveAndBackButton?: () => void;
}

export const ToolsDetails: React.FC<IToolsDetailsProps> = ({
	textButtonNew = "Nova",
	hideButtonNew = false,
	hideButtonBack = true,
	hideButtonSave = false,
	hideButtonDelete = false,
	hideButtonSaveAndBack = false,

	hideButtonNewLoading = false,
	hideButtonBackLoading = false,
	hideButtonSaveLoading = false,
	hideButtonDeleteLoading = false,
	hideButtonSaveAndBackLoading = false,

	onclickNewButton,
	onclickBackButton,
	onclickSaveButton,
	onclickDeleteButton,
	onclickSaveAndBackButton,
}) => {
	const smDown = useMediaQuery((theme: Theme) => theme.breakpoints.down("sm"));
	const mdDown = useMediaQuery((theme: Theme) => theme.breakpoints.down("md"));
	const theme = useTheme();

	return (
		<Box
			gap={1}
			marginX={1}
			padding={1}
			paddingX={2}
			display="flex"
			alignItems="center"
			height={theme.spacing(5)}
			component={Paper}
		>
			{(hideButtonSave && !hideButtonSaveLoading) && (
				<Button
					color='primary'
					disableElevation
					variant='contained'
					onClick={onclickSaveButton}
					startIcon={<SaveIcon />}
				>
					<Typography variant="button" whiteSpace="nowrap" textOverflow="ellipsis" overflow="hidden">
					Save
					</Typography>
				</Button>
			)}

			{hideButtonSaveLoading && (
				<Skeleton width={110} height={60} />
			)}

			{(hideButtonSaveAndBack && !hideButtonSaveAndBackLoading && !smDown && !mdDown) && (
				<Button
					color='primary'
					disableElevation
					variant='outlined'
					onClick={onclickSaveAndBackButton}
					startIcon={<SaveIcon />}
				>
					<Typography variant="button" whiteSpace="nowrap" textOverflow="ellipsis" overflow="hidden">
						Save and Back
					</Typography>
				</Button>
			)}

			{hideButtonSaveAndBackLoading && (
				<Skeleton width={180} height={60} />
			)}

			{(hideButtonDelete && !hideButtonDeleteLoading) && (
				<Button
					color='primary'
					disableElevation
					variant='outlined'
					onClick={onclickDeleteButton}
					startIcon={<DeleteIcon />}
				>
					<Typography variant="button" whiteSpace="nowrap" textOverflow="ellipsis" overflow="hidden">
						Delete
					</Typography>
				</Button>
			)}

			{hideButtonDeleteLoading && (
				<Skeleton width={110} height={60} />
			)}

			{(hideButtonNew && !hideButtonNewLoading && !mdDown) && (
				<Button
					color='primary'
					disableElevation
					variant='outlined'
					onClick={onclickNewButton}
					startIcon={<AddIcon />}
				>{textButtonNew}</Button>
			)}

			{hideButtonNewLoading && (
				<Skeleton width={110} height={60} />
			)}

			{(hideButtonBack && (hideButtonDelete || hideButtonNew || hideButtonSave || hideButtonSave || hideButtonSaveAndBack)
				&& (
					<Divider variant='middle' orientation='vertical' />
				)
			)}

			{(hideButtonBack && !hideButtonBackLoading) && (
				<Button
					color='primary'
					disableElevation
					variant='outlined'
					onClick={onclickBackButton}
					startIcon={<ArrowBackIcon />}
				>
					<Typography variant="button" whiteSpace="nowrap" textOverflow="ellipsis" overflow="hidden">
						Back
					</Typography>
				</Button>
			)}

			{hideButtonBackLoading && (
				<Skeleton width={110} height={60} />
			)}
		</Box>
	);
};