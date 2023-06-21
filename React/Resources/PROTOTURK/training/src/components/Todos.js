import { memo } from "react";
import TodoItem from "./TodoItem";
import '../tailwind.css';
function Todos({todos}) {

    console.log("TODOS rendered")
    
    return (
        <ul className="list-disc ml-4">
            {todos.map((todo, index) => <TodoItem todo={todo} key={index}/>)}
        </ul>
    );
}

export default memo(Todos);