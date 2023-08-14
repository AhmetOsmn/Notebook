import { createContext, useContext, useReducer } from "react";
import {siteReducer} from '../reducer';

const Context = createContext();

const Provider = ({ children }) => {

    const [state, dispatch] = useReducer(siteReducer, {
        theme: localStorage.getItem('theme') || 'light',
        language: localStorage.getItem('language') || 'tr'
    });

    // const [theme, setTheme] = useState('light');
    // const [language, setLanguage] = useState('tr');

    // const data = {
    //     theme,
    //     setTheme,
    //     language,
    //     setLanguage
    // }

    const data = {
        ...state,
        dispatch
    }

    return (
        <Context.Provider value={data}>
            {children}
        </Context.Provider>
    )
}

export const useSite = () => useContext(Context);

export default Provider;