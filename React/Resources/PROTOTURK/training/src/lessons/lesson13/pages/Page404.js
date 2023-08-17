import { Link } from "react-router-dom";

const Page404 = () => {
  return (
    <div >
      <h1 className="bg-red-400 mb-11">404 PAGE NOT FOUND</h1>
      <Link className="bg-blue-500 hover:bg-blue-600 text-white font-semibold py-2 px-4 rounded" to="/">Ana Sayfaya Dön</Link>
    </div>
  );
};

export default Page404;
