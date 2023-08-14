import Card from "../../components/Card";
import Footer from "./Footer";
import Header from "./Header";
import { useAuth } from "./context";

function Home() {

    const { user } = useAuth();

    return (
        <>
            <Card>
                <Header />
            </Card>
            App

            {user &&
                <Card>
                    <div>
                        Bu alan sadece giriş yapınca görünür.
                    </div>
                </Card>}
            <Card>
                <Footer />
            </Card>
        </>
    );
}

export default Home;