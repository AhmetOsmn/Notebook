import { useReducer, useState, useMemo, useCallback } from "react";
import Header from "../components/Header";
import CustomTab from "../components/CustomTab";
import AddTodo from "../components/AddTodo";
import Todos from "../components/Todos";
import '../tailwind.css';

function reducer(state, action) {
    switch (action.type) {
        case 'SET_TODO':
            return {
                ...state,
                todo: action.value
            }

        case 'ADD_TODO':
            return {
                ...state,
                todo: '',
                todos: [
                    ...state.todos,
                    action.todo
                ]
            }

        case 'SET_SEARCH':
            return {
                ...state,
                search: action.value
            }
    }
}

function EighthLesson() {

    console.log("eighth lesson rendered");

    const [count, setCount] = useState(0);

    const [state, dispatch] = useReducer(reducer, {
        todos: [],
        todo: ""
    });

    // const [todos, setTodos] = useState([]);
    // const [todo, setTodo] = useState('');

    const submitHandle = useCallback(e => {
        e.preventDefault();
        // setTodos([...todos, todo]);
        // setTodo("");
        dispatch({
            type: 'ADD_TODO',
            todo: state.todo
        })
    }, [state.todo])

    const onChange = useCallback(e => {
        // setTodo(e.target.value);
        dispatch({
            type: 'SET_TODO',
            value: e.target.value,
            search: ''
        });
    }, [])

    const searchHandle = e => {
        dispatch({
            type: 'SET_SEARCH',
            value: e.target.value
        })
    }

    const filteredTodos = useMemo(() => {
        return state.todos.filter(todo => todo.toLocaleLowerCase('TR').includes(state.search?.toLocaleLowerCase('TR')));
    }, [state.todos, state.search]);

    return (
        <div>
            <Header />
            <CustomTab />
            <h3>{count}</h3>
            <button className="rounded bg-red-300" onClick={() => setCount(count => count + 1)}>Arttır</button>
            <CustomTab />
            <input className="bg-blue-300 rounded p-2" type="text" value={state.search} placeholder="search.." onChange={searchHandle} />
            <CustomTab />
            <h1>Todo App</h1>
            <AddTodo onChange={onChange} submitHandle={submitHandle} todo={state.todo} />
            <Todos todos={filteredTodos} />
        </div>
    );
}

export default EighthLesson;
