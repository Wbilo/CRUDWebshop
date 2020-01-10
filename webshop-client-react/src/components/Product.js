import * as React from "react";


// Benytter function istedet for class da dette component ikke arbejder
// med en state og er mere simpelt  
function Product(props) {
        var spanStockClass = props.inStock ? "spanInStock" : "";
        var emStockClass =  props.inStock ? "emInStock" : "";
        var stockText = props.inStock ? "In Stock" : "Not In Stock"
        return (
            <div className="column"> 
                <h3>{props.name}</h3>
                <h4>{props.brand}</h4>
                <em className={emStockClass}><span className={spanStockClass}></span>{stockText}</em> 
                    <img src={props.img} alt=""/> 
                <button className="productButton" onClick={props.handleClick}>Add to cart</button>
                <p>{props.price} kr.</p> 
            </div>   
        ) 
}

export default Product;    