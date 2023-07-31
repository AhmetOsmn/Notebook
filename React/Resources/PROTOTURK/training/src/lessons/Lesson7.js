import { forwardRef, useRef } from "react";

// function Input(props, ref)
// {
//   return <input type="text" ref={ref} {...props} />
// }

// Input = forwardRef(Input);

// veya 👇 

const Input = forwardRef((props, ref) => {
    return <input ref={ref} type="text" {...props} />
})

function SeventhLesson() {

    const inputRef = useRef();

    const focusInput = () => {
        inputRef.current.focus();
    }

    return (
        <>
            <h1>useRef() - forwardRef()</h1>
            <p>useRef: JSX elemanlarını ref'lemek için kullanılır.</p>
            <p>forwardRef: Component'leri ref'lemek için kullanılır.</p>
            <Input ref={inputRef} />
            <button onClick={focusInput}>Focus</button>
        </>
    );
}

export default SeventhLesson;

