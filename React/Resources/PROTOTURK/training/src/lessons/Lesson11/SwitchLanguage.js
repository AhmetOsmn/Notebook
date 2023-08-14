import Button from '../../components/Button';
import Card from '../../components/Card';
import { useSite } from './context';

function SwitchLanguage() {

    const { language, dispatch } = useSite();

    const switchLanguage = () => {
        dispatch({
            type: 'TOGGLE_LANGUAGE',
        })
    }

    return (
        <Card bg="red">
            Mevcut Dil = {language} <br />
            <Button variant='danger' onClick={switchLanguage} text="dili değiştir" />
        </Card>
    );
}

export default SwitchLanguage;