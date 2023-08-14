import { createContext, useContext, useReducer } from "react";
import {authReducer} from '../reducer';

const Context = createContext();

const Provider = ({ children }) => {

    // const [user, setUser] = useState(false);

    // const data = {
    //     user, 
    //     setUser
    // }

    const [state, dispatch] = useReducer(authReducer, {
        user: localStorage.getItem('user') ? JSON.parse(localStorage.getItem('user')) :  false
    })

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

export const useAuth = () => useContext(Context);

export default Provider;