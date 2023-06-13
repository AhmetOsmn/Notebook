import classNames from "classnames";

function button({text, variant = 'default', setActiveTab}) {
    return (
        <button onClick={() => setActiveTab(2)} className={classNames({
            "p-4 h-10 flex items-cente rounded": true,
            "bg-gray-100": variant === 'default',
            "bg-green-600": variant === 'success',
            "bg-red-600": variant === 'danger',
            "bg-orange-300": variant === 'warning',

        })} >{text}</button>
    );
}

export default button;