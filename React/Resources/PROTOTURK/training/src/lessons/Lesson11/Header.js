import SwitchTheme from "./SwitchTheme";
import SwitchLanguage from "./SwitchLanguage";
import { useAuth } from "./context";
import Button from "../../components/Button";

function Header() {

    const { user, dispatch } = useAuth();

    const login = () => {
        dispatch({
            type: 'LOGIN',
            payload: {
                name: 'ahmet',
                id: 1
            }
        })
    }

    const logout = () => {
        dispatch({
            type: 'LOGOUT'
        })
    }

    return (
        <header>
            HEADER
            {user && <Button text="Çıkış Yap" onClick={logout} /> || <Button text="Giriş Yap" onClick={login} />}
            <SwitchTheme />
            <SwitchLanguage />
        </header>
    );
}

export default Header;