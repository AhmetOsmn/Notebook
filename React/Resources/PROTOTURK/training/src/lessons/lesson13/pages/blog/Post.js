import { useParams } from "react-router-dom";

const Post = () => {

  // root olarak /blog/post/{parametreler} şeklinde gönderilen parametreleri almamızı sağlayan hook.
  const params = useParams();
  console.log("🚀 ~ file: Post.js:6 ~ Post ~ params:", params)

  return (
    <div>
      Blog Post
    </div>
  );
};

export default Post;
