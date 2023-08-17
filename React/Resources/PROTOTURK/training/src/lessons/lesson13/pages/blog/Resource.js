import { useParams } from "react-router-dom";

const Resource = () => {

    const params = useParams();
    console.log("🚀 ~ file: Resource.js:6 ~ Resource ~ params:", params)

    const {url, id } = useParams();
    console.log("🚀 ~ file: Resource.js:9 ~ Resource ~ url, id:", url, id)

  return (
    <div>
      Resource
    </div>
  );
};

export default Resource;
