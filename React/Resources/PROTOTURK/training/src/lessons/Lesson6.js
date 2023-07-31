import { useState } from 'react';
import Test from '../components/Test';

function SixthLesson() {

    const [show, setShow] = useState(false);

    return (
        <>
            <button onClick={() => setShow(show => !show)}>{show ? "Gizle" : "Göster"}</button>
            {show && <Test />}
        </>
    );
}

export default SixthLesson;