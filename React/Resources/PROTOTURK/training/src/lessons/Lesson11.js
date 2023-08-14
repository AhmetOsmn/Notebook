import {SiteProvider, AuthProvider} from './Lesson11/context';
import Home from './Lesson11/Home';

function Lesson11() {

    // const [theme, setTheme] = useState('light');
    // const [language, setLanguage] = useState('tr');

    // const data = {
    //     theme,
    //     setTheme,
    //     language,
    //     setLanguage
    // }

    return (
        <SiteProvider>
            <AuthProvider>
                <Home />
            </AuthProvider>
        </SiteProvider>
    );
}

export default Lesson11;