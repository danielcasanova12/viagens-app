import * as React from "react";
import AppBar from "@mui/material/AppBar";
import Box from "@mui/material/Box";
import Toolbar from "@mui/material/Toolbar";
import IconButton from "@mui/material/IconButton";
import Typography from "@mui/material/Typography";
import Menu from "@mui/material/Menu";
import MenuIcon from "@mui/icons-material/Menu";
import Container from "@mui/material/Container";
import Avatar from "@mui/material/Avatar";
import Button from "@mui/material/Button";
import Tooltip from "@mui/material/Tooltip";
import MenuItem from "@mui/material/MenuItem";
import AdbIcon from "@mui/icons-material/Adb";
import { Link } from "react-router-dom";
import { useAppThemeContext } from "../../contexts/ThemeContext";
import DarkModeIcon from "@mui/icons-material/DarkMode";
import { useAuthContext  } from "../../contexts/AuthContext";
import { useNavigate } from "react-router-dom";
import { Dialog, DialogActions, DialogContent, DialogContentText, DialogTitle } from "@mui/material";
import ShoppingCartIcon from "@mui/icons-material/ShoppingCart";
import Badge from "@material-ui/core/Badge";
import { ReservationService } from "../../services/api/reservation/ReservationService";

const pages = ["dashboard", "voos", "hotels", "carros"];
const settings = ["Profile", "Logout"];




function ResponsiveAppBar() {
	const navigate = useNavigate();
	const {  logout } = useAuthContext();
	const [anchorElNav, setAnchorElNav] = React.useState<null | HTMLElement>(null);
	const [anchorElUser, setAnchorElUser] = React.useState<null | HTMLElement>(null);
	const { user} = useAuthContext ();
	const [openLogoutDialog, setOpenLogoutDialog] = React.useState(false);
	const [totalReservations, setTotalReservations] = React.useState<number>(0);
	const handleOpenNavMenu = (event: React.MouseEvent<HTMLElement>) => {
		setAnchorElNav(event.currentTarget);
	};
	const handleOpenUserMenu = (event: React.MouseEvent<HTMLElement>) => {
		setAnchorElUser(event.currentTarget);
	};


	const handleCloseNavMenu = () => {
		setAnchorElNav(null);
	};


	const handleCloseUserMenu = () => {
		setAnchorElUser(null);
	};


	const handleLogout = () => {
		localStorage.removeItem("APP_USER");
		localStorage.removeItem("APP_ACCESS_TOKEN");
		handleCloseUserMenu();
		setOpenLogoutDialog(false);
		logout();

	};
	const handleClick = () => {
		navigate("/shoppingCart");
	};

	const handleProfile = () => {
		// Implemente a lógica de logout aqui
		
		handleCloseUserMenu();
		navigate("/profile");
		setOpenLogoutDialog(false);
	};
	const handleOpenLogoutDialog = () => {
		handleCloseUserMenu();
		setOpenLogoutDialog(true);
	};

	const handleCloseLogoutDialog = () => {
		setOpenLogoutDialog(false);
	};

	const {toggleTheme} = useAppThemeContext();

	React.useEffect(() => {
		const fetchReservations = async () => {
			const userId = 1; // Substitua pelo ID do usuário atual
			const result = await ReservationService.getReservationsByUserId(userId);
			console.log(result);
			if (result && "reservations" in result && Array.isArray(result.reservations)) {
				setTotalReservations( result.totalReservations );
			} else {
				console.error(result);
			}
		};

		fetchReservations();
	}, []);

	return (
		<AppBar position="static">
			<Container maxWidth="xl">
				<Toolbar disableGutters>
					<AdbIcon sx={{ display: { xs: "none", md: "flex" }, mr: 1 }} />
					<Typography
						variant="h6"
						noWrap
						component="a"
						sx={{
							mr: 2,
							display: { xs: "none", md: "flex" },
							fontFamily: "monospace",
							fontWeight: 700,
							letterSpacing: ".3rem",
							color: "inherit",
							textDecoration: "none",
						}}
					>
            LOGO1
					</Typography>


					<Box sx={{ flexGrow: 1, display: { xs: "flex", md: "none" } }}>
						<IconButton
							size="large"
							aria-label="account of current user"
							aria-controls="menu-appbar"
							aria-haspopup="true"
							onClick={handleOpenNavMenu}
							color="inherit"
						>
							<MenuIcon />
						</IconButton>
						<Menu
							id="menu-appbar"
							anchorEl={anchorElNav}
							anchorOrigin={{
								vertical: "bottom",
								horizontal: "left",
							}}
							keepMounted
							transformOrigin={{
								vertical: "top",
								horizontal: "left",
							}}
							open={Boolean(anchorElNav)}
							onClose={handleCloseNavMenu}
							sx={{
								display: { xs: "block", md: "none" },
							}}
						>
							{pages.map((setting) => (
								<MenuItem
									key={setting}
									onClick={() => {
										handleCloseNavMenu();
										navigate(`/${setting.toLowerCase()}`);
                   
									}}
								>
									<Typography textAlign="center">{setting}</Typography>
								</MenuItem>
							))}
						</Menu>
					</Box>
					<AdbIcon sx={{ display: { xs: "flex", md: "none" }, mr: 1 }} />
					<Typography
						variant="h5"
						noWrap
						component="a"
						sx={{
							mr: 2,
							display: { xs: "flex", md: "none" },
							flexGrow: 1,
							fontFamily: "monospace",
							fontWeight: 700,
							letterSpacing: ".3rem",
							color: "inherit",
							textDecoration: "none",
						}}
					>
            LOGO2
					</Typography>
					<Box sx={{ flexGrow: 1, display: { xs: "none", md: "flex" } }}>
						{pages.map((page) => (
							<Link key={page} to={`/${page}`} style={{ textDecoration: "none" }}>
								<Button sx={{  my: 2, color: "white", display: "block" }}>
									{page}
								</Button>
							</Link>
						))}
					</Box>


					<Box sx={{ flexGrow: 0 }}>
						<IconButton aria-label="DarkMode" onClick={toggleTheme}>
							<DarkModeIcon />
						</IconButton>
						<IconButton onClick={handleClick}>
							<Badge badgeContent={totalReservations} color="secondary">
								<ShoppingCartIcon/>
							</Badge>
						</IconButton>
						<Tooltip title="Open settings">
							<IconButton onClick={handleOpenUserMenu} sx={{ p: 0 }}>
								<Avatar src={user?.image} alt={user?.username}  />
							</IconButton>
						</Tooltip>
						<Menu
							id="menu-appbar"
							anchorEl={anchorElUser}
							anchorOrigin={{
								vertical: "top",
								horizontal: "right",
							}}
							keepMounted
							transformOrigin={{
								vertical: "top",
								horizontal: "right",
							}}
							open={Boolean(anchorElUser)}
							onClose={handleCloseUserMenu}
						>
							{settings.map((setting) => (
								<MenuItem key={setting} onClick={setting === "Logout" ? handleOpenLogoutDialog : handleProfile}>
									{setting}
								</MenuItem>
							))}
						</Menu>
						<Dialog
							open={openLogoutDialog}
							onClose={handleCloseLogoutDialog}
						>
							<DialogTitle>
          Confirmar Logout
							</DialogTitle>
							<DialogContent>
								<DialogContentText>
            Você tem certeza de que deseja sair?
								</DialogContentText>
							</DialogContent>
							<DialogActions>
								<Button onClick={handleCloseLogoutDialog}>
            Cancelar
								</Button>
								<Button onClick={handleLogout} color="primary" autoFocus>
            Sair
								</Button>
							</DialogActions>
						</Dialog>
					</Box>
				</Toolbar>
			</Container>
		</AppBar>
	);
}






export default ResponsiveAppBar;

