import { memo } from 'react';
import '../tailwind.css';

function TodoItem({todo}) {

    console.log("TODO ITEM rendered")

    return (
        <li>{todo}</li>
    );
}

export default memo(TodoItem);