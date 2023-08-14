import Button from '../../components/Button'
import Card from '../../components/Card';
import { useSite } from './context';

function SwitchTheme() {

    const { theme, dispatch } = useSite();

    const switchTheme = () => {
        dispatch({
            type: 'TOGGLE_THEME',
        })
    }

    return (
        <Card bg="green">
            Mevcut tema = {theme} <br />
            <Button onClick={switchTheme} text="temayı değiştir" />
        </Card>
    );
}

export default SwitchTheme;