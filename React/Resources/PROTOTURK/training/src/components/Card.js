function Card({ children, bg }) {

    const cardClass = `bg-${bg || 'gray'}-200 shadow-md rounded-lg p-6 mb-2`

    return (
        <div className={cardClass}>
        {/* <div className="bg-red-400 shadow-md rounded-lg p-6 mb-2"> */}
            {children}
        </div>
    );
}

export default Card;