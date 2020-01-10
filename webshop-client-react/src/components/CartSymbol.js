import React from 'react';  
import "../css/cartImage.css";    
import cartImg from '../img/cart.png'; // with import 
import { Link } from 'react-router-dom';  
class CartSymbol extends React.Component { 
    constructor(props) {
      super(props);  
      
      this.state = {
        categories: []
      };
    } 
    render() { 
        return (
            <Link to="/cart">
                <img id="cartImg" alt="" src={cartImg}></img> 
                <span id='lblCartCount'>{this.props.qty}</span>
            </Link>
        );      
    } 
  }
  
  export default CartSymbol;  