import { useEffect, useState } from "react";

export default function Test() {

    const [postId, setPostId] = useState(1);
    const [post, setPost] = useState(false);

    // useEffect(() => {
    //     console.log("component render oldu.")
    // })

    useEffect(() => {
        fetch('https://jsonplaceholder.typicode.com/todos/1')
            .then(response => response.json())
            .then(json => console.log(json));

        let interval = setInterval(() => console.log('interval çalıştı!'),1000);
        
        return () => {
            console.log("component destroyed");
            clearInterval(interval);
        }
    }, [])

    useEffect(() => {
        fetch(`https://jsonplaceholder.typicode.com/posts/${postId}`)
            .then(res => res.json())
            .then(data => setPost(data))
    }, [postId])



    return (
        <div>
            <h3>{postId}</h3>
            <div>{post && JSON.stringify(post)}</div>
            <button onClick={() => setPostId(postId => postId + 1)}>Sonraki Konu</button>
            <hr />
            Test componenti
        </div>
    );
}
