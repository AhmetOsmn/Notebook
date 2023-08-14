import classNames from "classnames";

function button({ text, variant = 'success', onClick, disabled }) {
    return (
        <button
            disabled={disabled}
            onClick={onClick}
            className={classNames(
                "p-4 h-10 flex items-center rounded mb-3 mt-3",
                {
                    "bg-gray-100": disabled,
                    "bg-green-600": !disabled && variant === 'success',
                    "bg-red-600": !disabled && variant === 'danger',
                    "bg-orange-300": !disabled && variant === 'warning',
                }
            )}
        >
            {text}
        </button>

    );
}

export default button;