function Card({ children }) {
    return (
        <div className="bg-gray-400 shadow-md rounded-lg p-6 mb-2">
            {children}
        </div>
    );
}

export default Card;