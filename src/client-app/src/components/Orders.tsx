import React, { useEffect, useState } from 'react';

import {
    ApolloClient,
    InMemoryCache,
    gql
} from "@apollo/client";

interface OrderModel {
    id: string;
    orderId: string;
    number: string;
    description: string;
    status: string;
}

const client = new ApolloClient({
    uri: 'https://localhost:6001/graphql',
    cache: new InMemoryCache()
});

const Orders = () => {
    const [orders, setAppState] = useState<OrderModel[]>([]);

    useEffect(() => {
        client
            .query({
                query: gql`
                query{
                    get {
                        id
                        number
                        description
                        status
                    }
                }
           `
            })
            .then(result => {
                setAppState(result.data.get);
            });
    }, [setAppState]);

    return (
        <div>
            <h1 id="tabelLabel" >Orders</h1>
            <table className='table table-striped' aria-labelledby="tabelLabel">
                <thead>
                    <tr>
                        <th>Number</th>
                        <th>Description</th>
                        <th>Status</th>
                    </tr>
                </thead>
                <tbody>
                    {orders.map(order =>
                        <tr key={order.id}>
                            <td>{order.number}</td>
                            <td>{order.description}</td>
                            <td>{order.status}</td>
                        </tr>
                    )}
                </tbody>
            </table>
        </div>
    );
};

export default Orders;