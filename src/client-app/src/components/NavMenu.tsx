import { Navbar, Nav, Container } from 'react-bootstrap'

export default function NavMenu() {
    return (
        <Navbar bg="light" variant="light">
            <Container>
                <Navbar.Brand href="/">Point</Navbar.Brand>
                <Nav className="me-auto">
                    <Nav.Link href="/points">Points</Nav.Link>
                    <Nav.Link href="/orders">Orders</Nav.Link>
                </Nav>
            </Container>
        </Navbar>
    );
}