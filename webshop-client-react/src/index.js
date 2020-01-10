import React from 'react';
import ReactDOM from 'react-dom';
import { Route, BrowserRouter as Router } from 'react-router-dom'; 
import Header from './components/Header';
import ProductList from "./components/ProductList";
import Cart from './components/Cart';
import CartSymbol from './components/CartSymbol';
import * as serviceWorker from './serviceWorker'; 
import './css/index.css';    
import "./css/products.css";  
import "./css/Categories.css";   
import "./css/header.css";         
import "./css/ShoppingCart.css";         
import "./css/main.css";    
import { WebshopEndpoint } from "./constants";    
  
 
class WebShop extends React.Component {
    constructor(props) {
        super(props);

        this.state = { 
            qty: 0, 
            cartID: 1
        };  
        this.updateTotalCartItemQuan = this.updateTotalCartItemQuan.bind(this); 
        this.addToCartUsingPost = this.addToCartUsingPost.bind(this); 
    } 

    
    // kalder bestemt endpoint der skaber item i customer cart 
    addToCartUsingPost(prodID) {  

        let cartID = this.state.cartID;   
        const url = WebshopEndpoint + "/ShoppingCartController/AddItemToCart"; 
        // object som vi sender afsted i vores post request. 
        var cartObj = {cartID : cartID, productID: prodID}; 
        fetch(url, {
            method: "POST", 
            headers: {
              "Content-Type": "application/json",
              dataType: "application/json"
            }, 
            body: JSON.stringify(cartObj)
          }).then(
            // ved at benytte anonym arrow func så bliver funktionen først kaldt efter 
            // at fetch(url) er færdig behandlet (callback)
            () => this.updateTotalCartItemQuan()
            ) 
            .catch(() => { 
                console.log("An error occured");
            });   
    } 
    // Bliver kaldt når dette component bliver indsat ind i dom træet for første gang
    componentDidMount() {
        this.updateTotalCartItemQuan();   
    }

    // Kalder endpoint som retunere summen af items i customer cart 
    updateTotalCartItemQuan() {

        const url = WebshopEndpoint + "/ShoppingCartController/GetSumOfCartItems/" +
        this.state.cartID;  
        fetch(url)
            .then(response => response.json())
            .then(json => {
       
                this.setState({ qty: json })
            }
            ) 
            .catch(() => {
                console.log("An error occured");
            }); 

    }

    // Kaldes hver gang this.state ændrer sig 
    render() {

        return (
            <div>
               {/* React Router holder vores UI i sync med URL'en*/}
                <Router>
                    <Header>    
                    <CartSymbol 
                    qty = {this.state.qty} 
                    />   
                    </Header>
                    {/* route hvor man har et componenent
                    som man skal parse props til,
                     bliver nødt til at blive skrevet
                    på følgende måde med render method istedet for
                    "component={ProductList}"*/} 
                    <Route exact path="/" 
                    render={(props) => <ProductList 
                        {...props} 
                    addToCart = {this.addToCartUsingPost}
                    />}/> 

                    <Route exact path="/Brand/" 
                    render={(props) => <ProductList 
                        {...props} 
                    addToCart = {this.addToCartUsingPost}
                    />}/> 
                     {/* Når vi befinder os i f.eks. http://localhost:3000/cart path i URL'en
                    så bliver Cart component rendered*/} 
                    <Route path="/cart" render={(props) => <Cart 
                    {...props}  
                    updateTotalItemQuan = {this.updateTotalCartItemQuan}
                    />}/>   
                </Router>
            </div>  
        );

    }

} 

ReactDOM.render(<WebShop/>, document.getElementById('root'));

serviceWorker.unregister(); 
