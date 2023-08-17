import { NavLink, Route, Routes } from 'react-router-dom';
import Home from './lesson13/pages/Home'
import Page404 from './lesson13/pages/Page404'
import Blog from './lesson13/pages/blog/Blog'
import BlogLayout from './lesson13/pages/blog'
import Categories from './lesson13/pages/blog/Categories'
import Post from './lesson13/pages/blog/Post'
import Contact from './lesson13/pages/Contact'
import Resource from './lesson13/pages/blog/Resource';
import Blog404 from './lesson13/pages/blog/Blog404';
const Lesson13  = () => {

    return (
        <>
        <nav className='mt-2 mb-2'>
            <NavLink className="mr-3 p-2 bg-slate-500 rounded-xl" to="/">Home</NavLink>
            <NavLink className="mr-3 p-2 bg-slate-500 rounded-xl" to="/blog">Blog</NavLink>
            <NavLink className="mr-3 p-2 bg-slate-500 rounded-xl" to="/contact">Contact</NavLink>
        </nav>
        <Routes>
            <Route path='/' element={<Home/>} />
            <Route path='/blog' element={<BlogLayout/>}>
                <Route index={true} element={<Blog/>} />
                <Route path='categories' element={<Categories/>}/>

                {/* post/test1 */}
                {/* post/test2 */}
                {/* post/test3 */}
                {/* Yukarıdaki gibi dinamik url'ler için alt kısımdaki kullanım uygulanır. */}
                <Route path='post/:url'  element={<Post/>}/> 
                <Route path='resource/:url/:id'  element={<Resource/>}/> 
                <Route path='*' element={<Blog404/>} />
            </Route>
            <Route path='/contact' element={<Contact/>} />
            <Route path='*' element={<Page404/>} />
        </Routes>
        </>
        
    );
}


export default Lesson13;