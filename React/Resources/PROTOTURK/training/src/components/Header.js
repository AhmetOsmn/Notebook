import { memo } from "react";

function Header({ text }) {

    return (
        <header className="bg-gray-800 text-white py-4 px-8 mb-5">
            <h1 className="text-4xl font-bold">{text}</h1>
        </header>
    );
}

export default memo(Header);