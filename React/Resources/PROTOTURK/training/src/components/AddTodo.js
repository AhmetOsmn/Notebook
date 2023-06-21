import { memo } from 'react';
import '../tailwind.css';

function AddTodo({ submitHandle, onChange, todo }) {
   
    console.log("ADDTODO rendered")
    
    return (
        <form onSubmit={submitHandle}>
            <input className="bg-green-200 p-2 rounded" type="text" value={todo}  onChange={onChange} placeholder="todo" />
            <button className="bg-slate-400 p-2 rounded" disabled={!todo} type="submit">Ekle</button>
        </form>
    );
}

export default memo(AddTodo);